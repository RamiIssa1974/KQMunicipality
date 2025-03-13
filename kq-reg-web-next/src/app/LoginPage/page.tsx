"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { useStudentRegistration } from "../../context/StudentRegistrationContext";
import { searchStudent } from "../services/studentService";
import './LoginPage.css';
export default function LoginPage() {
    const router = useRouter();
    const [idnError, setIdnError] = useState<string | null>(null);

    const { selectedId, selectedYear, studentIdn, setStudentIdn, studentBirthDate, setStudentBirthDate } = useStudentRegistration();

    const [errorMessage, setErrorMessage] = useState("");

    // Load from localStorage if needed
    useEffect(() => {
        const savedIdn = localStorage.getItem("studentIdn");
        const savedBirthDate = localStorage.getItem("studentBirthDate");

        if (savedIdn) setStudentIdn(savedIdn);
        if (savedBirthDate) setStudentBirthDate(savedBirthDate);
    }, []);

    const validateIDN = (idn: string): boolean => {
        const idnPattern = /^\d{9}$/;
        return idnPattern.test(idn);
    };

    const handleSearch = async () => {
        setIdnError(null);
        setErrorMessage("");

        if (!validateIDN(studentIdn)) {
            setIdnError("رقم الهوية يجب أن يكون 9 أرقام فقط");
            return;
        }

        if (!studentIdn || !studentBirthDate) {
            setErrorMessage("الرجاء تعبئة جميع الحقول");
            return;
        }

        if (!selectedYear) {
            setErrorMessage("خطأ: لم يتم العثور على سنة التسجيل");
            return;
        }

        try {
            const student = await searchStudent(studentIdn, studentBirthDate, selectedYear);

            if (!student) {
                // This is an expected situation, not an "error"
                console.warn("Student not found!");
                setErrorMessage("لم يتم العثور على بيانات الطالب, الرجاء التحقق من رقم الهوية وتاريخ الميلاد اولا.ثم التوجه الى مركز الاتصالات لبلدية كفر قاسم:");
                return;
            }

            router.push("/Register");
        } catch (error) {
            // Only use console.error for unexpected exceptions
            console.error("Unexpected error in handleSearch:", error);
            setErrorMessage("حدث خطأ أثناء الاتصال بالخادم، حاول مرة أخرى");
        }
    };

    

    return (
        <div className="login-page-container">

            {errorMessage && (
                <div className="login-error-message-container">
                    <span>{errorMessage}</span>

                    <div className="login-error-buttons">
                        <a className="login-error-search-message-btn" href="tel:*3595">
                            <img src="/images/kq-telefon-btn.png" alt="Phone" />
                        </a>
                        <a className="login-error-search-message-btn" href="https://wa.me/972502201681" target="_blank" rel="noopener noreferrer">
                            <img src="/images/kq-whatsapp-btn.png" alt="WhatsApp" />
                        </a>
                    </div>
                </div>
            )}

            <header className="login-header">
                <h1>نافذة تسجيل الطلاب</h1>
            </header>

            <div className="login-main-container">
                <img src="/Images/kq-kidsgirl.png" alt="Kid Left" className="login-image login-image-left" />

                <div className="login-form-container">

                    {/* Student IDN */}
                    <div className="login-input-wrapper">
                        <label>תעודת זהות / رقم هوية الطالب</label>
                        <input
                            type="text"
                            value={studentIdn}
                            onChange={(e) => setStudentIdn(e.target.value)}
                            placeholder="أدخل رقم الهوية"
                        />
                        {idnError && (
                            <p className="login-idn-error">{idnError}</p>
                        )}
                    </div>

                    {/* Birth Date */}
                    <div className="login-input-wrapper">
                        <label>תאריך לידה / تاريخ الميلاد الطالب</label>
                        <input
                            type="date"
                            value={studentBirthDate}
                            onChange={(e) => setStudentBirthDate(e.target.value)}
                            placeholder="تاريخ ولادة / تاريخ الميلاد الطالب"
                        />
                    </div>

                    {/* Search Button */}
                    <button onClick={handleSearch} className="login-search-button">
                        חיפוש / اضغط للبحث
                    </button>
                </div>

                <img src="/Images/kq-kidsboy.png" alt="Kid Right" className="login-image login-image-right" />
            </div>
        </div>
    );

}
