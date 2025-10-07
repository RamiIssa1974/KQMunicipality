"use client";
import { StudentRequest } from "@/types";
import { API_BASE } from '../services/config';

export const submitRejection = async (studentRequest: StudentRequest) => {
    try {
        const response = await fetch(`/api/Schools/students`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(studentRequest)
        });

        if (!response.ok) {            
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const responseText = await response.text();
        console.log("Submit response:", responseText);
        if (!response.ok || responseText.includes("Failed")) {
            throw new Error(responseText); // Manual failure trigger
        }
        return responseText; // Return result only if successful
    } catch (error) {
        console.error("Error submitting rejection:", error);
        throw error; // Propagate the error to the caller
    }
};

export const getSchools = async () => {
    try {
        const response = await fetch(`/api/Schools`);
        if (!response.ok) {
            throw new Error("Failed to fetch schools");
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching schools:", error);
        return [];
    }
};
export const getTeachers = async (schoolId: string, year: number, registrationType: number) => {
    try {
        const response = await fetch(`/api/Schools/teachers/${schoolId}/${year}/${registrationType}`);
        if (!response.ok) {
            throw new Error("Failed to fetch teachers");
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching teachers:", error);
        return [];
    }
};

export const submitRegistration = async (requestData: any) => {
    try {
        const response = await fetch(`/api/Schools/students`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(requestData)
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const responseText = await response.text();

        return responseText;
    } catch (error) {
        console.error("Error submitting registration:", error);
        throw error;
    }
};

export const getRegistrationTypes = async () => {
    try {
        const apiUrl = `/api/schools/RegistrationTypes`;  

        console.log(`Fetching registration types from: ${apiUrl}`);

        const response = await fetch(apiUrl, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });

        console.log("Response status:", response.status, response.statusText);

        if (!response.ok) {
            throw new Error(`Failed to fetch registration types: ${response.status} ${response.statusText}`);
        }

        const data = await response.json();
        console.log("Fetched Registration Types:", data);
        data.forEach((item: any, index: number) => {
            console.log(`Item ${index} - Id: ${item.id}, ViewName: ${item.viewName}, IsActiv:${item.isActiv}, year: ${item.year}`);
        });
        const filteredData = data.filter((type: any) =>  type.isActiv );
        console.log("Filtered Registration Types:", filteredData);

        return filteredData;
    } catch (error) {
        console.error("Error fetching registration types:", error);
        return [];
    }
};
