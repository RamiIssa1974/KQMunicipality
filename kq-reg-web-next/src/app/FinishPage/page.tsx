"use client";

import React, { useEffect, useState } from "react";
import { searchStudent } from "../services/studentService"; // adjust path if needed
import { useStudentRegistration } from "../../context/StudentRegistrationContext";
import './FinishPage.css';
export default function FinishPage() {
    const [registrationDate, setRegistrationDate] = useState("");

    const { selectedYear } = useStudentRegistration();
    useEffect(() => {
        if (selectedYear === null || selectedYear === 0) {
            console.log("Waiting for selectedYear...");
            return;
        }
        const studentIdn = localStorage.getItem("studentIdn");
        const studentBirthDate = localStorage.getItem("studentBirthDate");
        const year = selectedYear ?? parseInt(localStorage.getItem("registrationYear") || "0", 10);

        console.log("studentIdn:", studentIdn);
        console.log("studentBirthDate:", studentBirthDate);
        console.log("year:", year);

        if (!studentIdn || !studentBirthDate || year === 0) {
            console.error("Missing student data!");
            return;
        }

        const fetchStudent = async () => {
            try {
                const student = await searchStudent(studentIdn, studentBirthDate, year);

                if (student && student.reshoum_hetsonee_bdekaa) {
                    const formattedDate = formatDateTime(student.reshoum_hetsonee_bdekaa);
                    setRegistrationDate(formattedDate);
                }
            } catch (error) {
                console.error("Error fetching student:", error);
            }
        };

        fetchStudent();
    }, [selectedYear]);

    const formatDateTime = (dateString: string) => {
        const date = new Date(dateString);

        const hours = date.getHours().toString().padStart(2, "0");
        const minutes = date.getMinutes().toString().padStart(2, "0");

        const day = date.getDate().toString().padStart(2, "0");
        const month = (date.getMonth() + 1).toString().padStart(2, "0");
        const year = date.getFullYear();

        return `${hours}:${minutes} ${day}/${month}/${year}`;
    };

    return (
        <div className="finish-page-container">
            <div className="finish-success-box">
                {/* Top Section - Success Message */}
                <div className="finish-message-section">
                    <p className="finish-message-title">
                        تم التسجيل بنجاح / הרישום בוצע בהצלחה
                    </p>
                    <p className="finish-message-date">
                        {registrationDate || "---"}
                    </p>
                </div>

                {/* Bottom Section - Checkmark */}
                <div className="finish-icon-section">
                    <div className="finish-checkmark-circle">
                        &#10003;
                    </div>
                </div>
            </div>
        </div>
    );


}
