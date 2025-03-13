"use client";

import React, {
    createContext,
    useContext,
    useState,
    useEffect,
    ReactNode
} from "react";

// Define the context type
type StudentRegistrationContextType = {
    selectedId: number | null;
    setSelectedId: (id: number) => void;

    selectedYear: number | null;
    setSelectedYear: (year: number) => void;

    studentIdn: string;
    setStudentIdn: (idn: string) => void;

    studentBirthDate: string;
    setStudentBirthDate: (birthDate: string) => void;

    clearRegistrationData: () => void;
};

// Create the context itself
const StudentRegistrationContext = createContext<
    StudentRegistrationContextType | undefined
>(undefined);

// The provider component
export const StudentRegistrationProvider = ({
    children
}: {
    children: ReactNode;
}) => {
    // States
    const [selectedId, setSelectedIdState] = useState<number | null>(null);
    const [selectedYear, setSelectedYearState] = useState<number | null>(null);
    const [studentIdn, setStudentIdnState] = useState<string>("");
    const [studentBirthDate, setStudentBirthDateState] = useState<string>("");

    // Load initial data from localStorage (on first mount)
    useEffect(() => {
        const storedId = localStorage.getItem("selectedRegistrationTypeId");
        const storedYear = localStorage.getItem("selectedRegistrationTypeYear");
        const storedIdn = localStorage.getItem("studentIdn");
        const storedBirthDate = localStorage.getItem("studentBirthDate");

        if (storedId) setSelectedIdState(parseInt(storedId, 10));
        if (storedYear) setSelectedYearState(parseInt(storedYear, 10));
        if (storedIdn) setStudentIdnState(storedIdn);
        if (storedBirthDate) setStudentBirthDateState(storedBirthDate);
    }, []);

    // Setters that update state + localStorage
    const setSelectedId = (id: number) => {
        setSelectedIdState(id);
        localStorage.setItem("selectedRegistrationTypeId", id.toString());
    };

    const setSelectedYear = (year: number) => {
        setSelectedYearState(year);
        localStorage.setItem("selectedRegistrationTypeYear", year.toString());
    };

    const setStudentIdn = (idn: string) => {
        setStudentIdnState(idn);
        localStorage.setItem("studentIdn", idn);
    };

    const setStudentBirthDate = (birthDate: string) => {
        setStudentBirthDateState(birthDate);
        localStorage.setItem("studentBirthDate", birthDate);
    };

    // Clear everything if needed (optional helper)
    const clearRegistrationData = () => {
        setSelectedIdState(null);
        setSelectedYearState(null);
        setStudentIdnState("");
        setStudentBirthDateState("");

        localStorage.removeItem("selectedRegistrationTypeId");
        localStorage.removeItem("selectedRegistrationTypeYear");
        localStorage.removeItem("studentIdn");
        localStorage.removeItem("studentBirthDate");
    };

    return (
        <StudentRegistrationContext.Provider
            value={{
                selectedId,
                setSelectedId,
                selectedYear,
                setSelectedYear,
                studentIdn,
                setStudentIdn,
                studentBirthDate,
                setStudentBirthDate,
                clearRegistrationData
            }}
        >
            {children}
        </StudentRegistrationContext.Provider>
    );
};

// Hook for consuming the context in components
export const useStudentRegistration = () => {
    const context = useContext(StudentRegistrationContext);
    if (!context) {
        throw new Error(
            "useStudentRegistration must be used within a StudentRegistrationProvider"
        );
    }
    return context;
};
