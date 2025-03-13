import { Student } from "@/types/Student";

export const searchStudent = async (
    id: string,
    birthDate: string,
    year: number
): Promise<Student | null> => {
    try {
        const apiUrl = `https://localhost:7032/api/Schools/students/${id}/${birthDate}/${year}`;

        const response = await fetch(apiUrl, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (response.status === 404) {
            console.warn("Student not found");
            return null;
        }

        if (!response.ok) {
            console.warn(`Server returned error ${response.status}`);
            return null;
        }

        const student = await response.json();
        return student;

    } catch (error) {
        console.error("searchStudent (catch block):", error);
        return null;
    }
}; // ✅ This is the missing closing curly brace!
