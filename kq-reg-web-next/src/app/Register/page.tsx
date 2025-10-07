"use client";

import { useEffect, useState } from "react";
import { useStudentRegistration } from "../../context/StudentRegistrationContext";
import { submitRegistration } from "../services/registrationService";
import { searchStudent } from "../services/studentService";

import { useRouter } from "next/navigation";
import './RegisterPage.css';
import { StudentRequest } from "@/types";
export default function RegisterPage() {
    const router = useRouter();

    const { selectedId, selectedYear } = useStudentRegistration();

    const [studentIdn, setStudentIdn] = useState("");
    const [studentBirthDate, setStudentBirthDate] = useState("");
    const [schoolName, setSchoolName] = useState("ss"); // Placeholder school
    const [managerName, setManagerName] = useState("ss"); // Placeholder manager
    const [agreement, setAgreement] = useState("agree"); // default: agree

    // Load data from localStorage on first render
    useEffect(() => {
        const storedIdn = localStorage.getItem("studentIdn");
        const storedBirthDate = localStorage.getItem("studentBirthDate");

        if (storedIdn) setStudentIdn(storedIdn);
        if (storedBirthDate) setStudentBirthDate(storedBirthDate);

        // TODO: You can fetch school and manager info from API if not hardcoded
    }, []);

    const handleSubmit = async () => {
        try {
            if (selectedYear === null || selectedId === null) {
                alert("الرجاء تحديد نوع التسجيل وسنة التسجيل");
                return;
            }
            const student = await searchStudent(studentIdn, studentBirthDate, selectedYear);
            if (!student) {
                alert("الطالب غير موجود، لا يمكن تحديث البيانات");
                return;
            }
            // Build the request object
            const requestBody: StudentRequest = {
                idn: student.idn,
                birthDate: student.birthDate?.split('T')[0],
                agree: agreement === "agree" ? "Yes" : "No",
                year: selectedYear,
                registration_Type: selectedId,
                reshoum_hetsonee_bdekaa: student.reshoum_hetsonee_bdekaa,
                schoolName: student.schoolName,
                schoolId: student.schoolId,
                teacherName: student.teacherName,
                teacherId: student.teacherId,
                firstAlternativeSchool: student.firstAlternativeSchool || "",
                firstAlternativeTeacher: student.firstAlternativeTeacher || "",
                secondAlternativeSchool: student.secondAlternativeSchool || "",
                secondAlternativeTeacher: student.secondAlternativeTeacher || "",
                reason: student.reason || "",
                endPoint: ""
            };


            console.log("Submitting updated request body:", requestBody);


            // Call the API
            const updatedStudent = await submitRegistration(requestBody);

            console.log("updatedStudent:", updatedStudent);
            // Handle the response & redirect
            if (agreement === "agree") {
                router.push("/FinishPage");
            } else {
                router.push("/RejectPage");
            }
        } catch (error) {
            console.error("Submit failed:", error);
            alert("حدث خطأ أثناء الإرسال، حاول مرة أخرى");
        }
    };



    return (
        <div className="register-page-container">
            {/* Header */}
            <div className="header-info-section">
                <img src="/Images/kq-KidsGirl.png" alt="Kid Left" className="header-image" />

                <div className="header-text-section">
                    <p className="header-line"><strong>תעודת זהות / رقم هوية الطالب:</strong></p>
                    <p className="header-line">{studentIdn}</p>

                    <p className="header-line"><strong>תאריך לידה / تاريخ الميلاد الطالب:</strong></p>
                    <p className="header-line">{studentBirthDate}</p>
                </div>

                <img src="/Images/kq-KidsBoy.png" alt="Kid Right" className="header-image" />
            </div>

            <hr />

            {/* Main Section */}
            <div className="main-section-container">
                {/* School & Manager Section */}
                <div className="school-manager-section">
                    <div className="school-manager-item">
                        <img src="/Images/kq-school-icon.png" alt="School Icon" />
                        <span><strong>בית ספר / المدرسة:</strong> {schoolName}</span>
                    </div>
                    <div className="school-manager-item">
                        <img src="/Images/kq-teacher-icon.png" alt="Manager Icon" />
                        <span><strong>מנהלת / المعلمة:</strong> {managerName}</span>
                    </div>
                </div>

                {/* Separator */}
                <div className="separator"></div>

                {/* Agreement Section */}
                <div className="agreement-section">
                    <label className="radio-option">
                        <input
                            type="radio"
                            name="agreement"
                            value="agree"
                            checked={agreement === "agree"}
                            onChange={() => setAgreement("agree")}
                        />
                        اوافق على التسجيل المقترح
                    </label>

                    <label className="radio-option">
                        <input
                            type="radio"
                            name="agreement"
                            value="disagree"
                            checked={agreement === "disagree"}
                            onChange={() => setAgreement("disagree")}
                        />
                        غير موافق، اريد اختيار مدرسة اخرى
                    </label>
                </div>
            </div>

            <hr />

            {/* Submit Button */}
            <div className="register-submit-button">
                <button onClick={handleSubmit}>
                    שליחה / إرسال
                </button>
            </div>
        </div>
    );

}
