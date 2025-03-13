"use client";
import { useRouter } from 'next/navigation';
import { useEffect, useState } from "react";
import { getRegistrationTypes } from "../services/registrationService";
import { useStudentRegistration } from '../../context/StudentRegistrationContext';
import './HomePage.css';

export default function Home() { 
    const router = useRouter();

    const { selectedId, setSelectedId, setSelectedYear } = useStudentRegistration();
    const [idnError, setIdnError] = useState<string | null>(null);
     
    const [registrationTypes, setRegistrationTypes] = useState<{ id: number; viewName: string, year: number }[]>([]);


    // Fetch registration types when the component mounts
    useEffect(() => {
        const fetchData = async () => {
            const data = await getRegistrationTypes();
            setRegistrationTypes(data);
        };

        fetchData();
    }, []);

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const id = Number(event.target.value);
        setSelectedId(id);
        const selectedType = registrationTypes.find((type) => type.id === id);

        if (selectedType) {
            setSelectedId(id);                
            setSelectedYear(selectedType.year);
        }
    };

    const handleSubmit = () => {
        if (selectedId) {
            router.push('/LoginPage');
        } else {
            alert('يجب اختيار نوع التسجيل أولاً');
        }
    };

    return (
        <div className="home-container">
            <h1>مرحبًا بك في بوابة تسجيل الطلاب</h1>

            {/* Dropdown Label */}
            <label htmlFor="registrationType">اختر نوع التسجيل:</label>

            <div className="home-select-container">
                <select
                    id="registrationType"
                    value={selectedId ?? ''}
                    onChange={handleSelectChange}
                    className="home-dropdown"
                >
                    <option value="">-- اختر خيارًا --</option>
                    {registrationTypes.map((type) => (
                        <option key={type.id} value={type.id}>
                            {type.viewName}
                        </option>
                    ))}
                </select>

                {/* Submit Button */}
                <button
                    onClick={handleSubmit}
                    className={`home-submit-button ${!selectedId ? 'disabled' : ''}`}
                    disabled={!selectedId}
                >
                    שליחה / ارسال
                </button>
            </div>
        </div>
    );


}
