import React, { useState, useEffect } from "react";
import medical_records from "./medicalRecords";

function Records({ recordId }) {
    const [selectedRecordId, setSelectedRecordId] = useState(0);
    const [selectedRecord, setSelectedRecord] = useState(null);

    useEffect(() => {
        console.log("recId:", recordId);
        if (recordId > 0) {
            setSelectedRecordId(recordId);
            const patRec = medical_records[recordId - 1];
            console.log("patRec", patRec);            
            if (patRec) {
                setSelectedRecord(patRec);
                //setSelectedRecordId(recordId)
            }
        }
    }, [recordId]);

    const handleNextClick = () => {
        setSelectedRecord(medical_records[selectedRecordId % medical_records.length]);
        setSelectedRecordId(selectedRecordId + 1);
    }

    if (!selectedRecord) return (<div>No data</div>);
    return (
        <div className="patient-profile-container" id="profile-view">
            <div className="layout-row justify-content-center">
                <div id="patient-profile" data-testid="patient-profile" className="mx-auto">
                    <h4 id="patient-name">{selectedRecord?.data[0].userName}</h4>
                    <h5 id="patient-dob">DOB: {selectedRecord?.data[0].userDob}</h5>
                    <h5 id="patient-height">Height: {selectedRecord?.data[0].meta.height}</h5>
                </div>
                <button className="mt-10 mr-10" data-testid="next-btn"
                    onClick={handleNextClick}
                >
                    Next
                </button>
            </div>

            <table id="patient-records-table">
                <thead id="table-header">
                    <tr>
                        <th>SL</th>
                        <th>Date</th>
                        <th>Diagnosis</th>
                        <th>Weight</th>
                        <th>Doctor</th>
                    </tr>
                </thead>
                <tbody id="table-body" data-testid="patient-table">
                    {selectedRecord?.data.map((rec, index) => (
                        <tr key={index}>
                            <td>{rec.id}</td>
                            <td>{new Date(rec.timestamp).toLocaleDateString()}</td>
                            <td>{rec.diagnosis.name}</td>
                            <td>{rec.meta.weight}</td>
                            <td>{rec.doctor.name}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Records;
