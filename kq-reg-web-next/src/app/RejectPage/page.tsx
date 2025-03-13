"use client";

import React, { useEffect, useState } from "react";
import { searchStudent } from "../services/studentService";
import { useStudentRegistration } from "../../context/StudentRegistrationContext";
import { getSchools, getTeachers, submitRejection } from "../services/registrationService";
 
import { School, Teacher, StudentRequest, Student } from "@/types";
import { useRouter } from "next/navigation";
import './RejectPage.css';

export default function RejectPage() {
    const router = useRouter();
    const { selectedId, selectedYear, studentIdn, studentBirthDate } = useStudentRegistration();

    const [student, setStudent] = useState<Student | null>(null);
    const [schools, setSchools] = useState<School[]>([]);
    const [firstChoiceTeachers, setFirstChoiceTeachers] = useState<Teacher[]>([]);
    const [secondChoiceTeachers, setSecondChoiceTeachers] = useState<Teacher[]>([]);

    const [firstChoiceSchool, setFirstChoiceSchool] = useState("");
    const [firstChoiceTeacher, setFirstChoiceTeacher] = useState("");
    const [secondChoiceSchool, setSecondChoiceSchool] = useState("");
    const [secondChoiceTeacher, setSecondChoiceTeacher] = useState("");
    const [rejectionReason, setRejectionReason] = useState("");
    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    useEffect(() => {
        if (selectedYear === null || selectedId === null) {
            console.log("Waiting for selectedYear/selectedId...");
            return;
        }
         
        if (!studentIdn || !studentBirthDate) {
            console.log("Missing student ID or birthDate");
            return;
        }

        const fetchData = async () => {
            try {
                const studentResult = await searchStudent(studentIdn, studentBirthDate, selectedYear);
                setStudent(studentResult);

                const schoolsResult = await getSchools();
                setSchools(schoolsResult);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, [selectedYear, selectedId]);

    const handleFirstChoiceSchoolChange = async (schoolId: string) => {
        if (selectedYear === null || selectedId === null) {
            console.log("Year or RegType is null. Waiting...");
            return;
        }

        setFirstChoiceSchool(schoolId);

        if (!schoolId) {
            setFirstChoiceTeachers([]);
            return;
        }

        const teachersResult = await getTeachers(schoolId, selectedYear, selectedId);
        setFirstChoiceTeachers(teachersResult);
    };

    const handleSecondChoiceSchoolChange = async (schoolId: string) => {
        if (selectedYear === null || selectedId === null) {
            console.log("Year or RegType is null. Waiting...");
            return;
        }

        setSecondChoiceSchool(schoolId);

        if (!schoolId) {
            setSecondChoiceTeachers([]);
            return;
        }

        const teachersResult = await getTeachers(schoolId, selectedYear, selectedId);
        setSecondChoiceTeachers(teachersResult);
    };

    const validateForm = (): boolean => {
        const errors: string[] = [];

        if (!firstChoiceSchool) errors.push("الرجاء اختيار المدرسة للأفضلية الأولى");
        if (!firstChoiceTeacher) errors.push("الرجاء اختيار المعلمة للأفضلية الأولى");
        if (!secondChoiceSchool) errors.push("الرجاء اختيار المدرسة للأفضلية التالية");
        if (!secondChoiceTeacher) errors.push("الرجاء اختيار المعلمة للأفضلية التالية");
        if (!rejectionReason.trim()) errors.push("الرجاء إدخال سبب الرفض");

        setValidationErrors(errors);
        return errors.length === 0;
    };

    const handleSubmit = async () => {
        if (!validateForm() || !student) return;

        const formattedBirthDate = student.birthDate.split('T')[0];

        const studentRequest: StudentRequest = {
            reshoum_hetsonee_bdekaa: student.reshoum_hetsonee_bdekaa,
            endPoint: "",
            year: student.year,
            idn: student.idn,
            birthDate: formattedBirthDate,
            agree: "No",
            schoolName: student.schoolName,
            schoolId: student.schoolId,
            teacherName: student.teacherName,
            teacherId: student.teacherId,
            firstAlternativeSchool: firstChoiceSchool,
            firstAlternativeTeacher: firstChoiceTeacher,
            secondAlternativeSchool: secondChoiceSchool,
            secondAlternativeTeacher: secondChoiceTeacher,
            reason: rejectionReason,
            registration_Type: student.registrationTypeId
        };

        try {
            console.log("studentRequest:",studentRequest);
            await submitRejection(studentRequest);

            // Only redirect if submission succeeded
            router.push("/FinishPage");
        } catch (error) {
            console.error("Submission failed:", error);

            // Show error message on the page
            setValidationErrors(["فشل إرسال الطلب، حاول مرة أخرى لاحقًا."]);
        }
    };

    const formatDate = (dateString: string | undefined) => {
        if (!dateString) return "---";
        const date = new Date(dateString);
        return `${date.getDate().toString().padStart(2, "0")}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`;
    };
    if (selectedYear === null || selectedId === null) return <div>Loading...</div>;
    return (
        <div className="reject-page-container">
            {/* Instruction text */}
            <p className="instructions">
                الرجاء ادخال امكانيّتين لتسجيل ابنك/ابنتك وسيتم فحص الطلب وإبلاغكم بقرار الجهة المسؤولة.
            </p>

            {/* Form Wrapper */}
            <div className="reject-form-wrapper">
                {/* First & Second Choices */}
                <div className="choices-section">
                    {/* First Choice */}
                    <div className="choice-column">
                        <h3>افضلية أولى:</h3>
                        <select
                            value={firstChoiceSchool}
                            onChange={(e) => handleFirstChoiceSchoolChange(e.target.value)}
                        >
                            <option value="">اختيار المدرسه</option>
                            {schools.map((school) => (
                                <option key={school.id} value={school.id}>
                                    {school.name}
                                </option>
                            ))}
                        </select>

                        <select
                            value={firstChoiceTeacher}
                            onChange={(e) => setFirstChoiceTeacher(e.target.value)}
                        >
                            <option value="">اختيار المعلمه</option>
                            {firstChoiceTeachers.map((teacher) => (
                                <option key={teacher.id} value={teacher.id}>
                                    {teacher.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    {/* Second Choice */}
                    <div className="choice-column">
                        <h3>افضلية تالية:</h3>
                        <select
                            value={secondChoiceSchool}
                            onChange={(e) => handleSecondChoiceSchoolChange(e.target.value)}
                        >
                            <option value="">اختيار المدرسه</option>
                            {schools.map((school) => (
                                <option key={school.id} value={school.id}>
                                    {school.name}
                                </option>
                            ))}
                        </select>

                        <select
                            value={secondChoiceTeacher}
                            onChange={(e) => setSecondChoiceTeacher(e.target.value)}
                        >
                            <option value="">اختيار المعلمه</option>
                            {secondChoiceTeachers.map((teacher) => (
                                <option key={teacher.id} value={teacher.id}>
                                    {teacher.name}
                                </option>
                            ))}
                        </select>
                    </div>
                </div>

                {/* Rejection Reason */}
                <div className="reason-section">
                    <h3>أسباب الرفض:</h3>
                    <textarea
                        value={rejectionReason}
                        onChange={(e) => setRejectionReason(e.target.value)}
                        placeholder="...اكتب أسباب الرفض هنا"
                        maxLength={255}
                    ></textarea>
                </div>

                {/* Submit Button */}
                <div className="submit-container">
                    <button className="submit-button" onClick={handleSubmit}>
                        שליחה / ارسال
                    </button>
                </div>

                {/* Validation Errors */}
                {validationErrors.length > 0 && (
                    <div className="validation-errors">
                        <ul>
                            {validationErrors.map((error, index) => (
                                <li key={index}>{error}</li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

            {/* Footer Info */}
            <div className="footer-info">
                <div className="footer-column">
                    <div>📞 טלפון הורים <strong>{student?.telephone || "---"}</strong></div>
                    <div>📅 תאריך לידה <strong>{formatDate(student?.birthDate)}</strong></div>
                    <div>🎓 שם תלמיד <strong>{student?.name || "---"}</strong></div>
                </div>
                <div className="footer-column">
                    <div>🆔 תעודת זהות <strong>{student?.idn || "---"}</strong></div>
                    <div>🏫 בית ספר <strong>{student?.schoolName || "---"}</strong></div>
                    <div>👩‍🏫 מורה <strong>{student?.teacherName || "---"}</strong></div>
                </div>
            </div>
        </div>
    );


}
