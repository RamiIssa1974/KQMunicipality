"use client";

import { useEffect, useState } from "react";


export default function TestPage() {
    const [count, setCount] = useState(0);
    const [autoCounter, setAutoCounter] = useState(0);
    const [isRunning, setIsRunning] = useState(true);

    useEffect(() => {
        let interval: NodeJS.Timeout;

        if (isRunning) {
            interval = setInterval(() => {
                setAutoCounter((prevCounter) => prevCounter + 1);
            }, 1000);
        }
        return () => clearInterval(interval);
    }, [isRunning]);
    const handleStop = () => setIsRunning(false);
    const handleResume = () => setIsRunning(true);

    useEffect(() => {
        console.log("Rami Issa: Page loaded");
        const fetchUsers = async () => {
            const response = await fetch('https://jsonplaceholder.typicode.com/users');
            const data = await response.json();
            console.log(data);
        };

        fetchUsers();
    }, []);

    const handleIncrement = () => { setCount(count + 1); }
    return (
        <div>
            <h1>Tester page</h1>
            <div>
                <h2>AutoCounter: {autoCounter}</h2>
                <button onClick={handleStop} style={{ marginRight: '10px' }}>Stop</button>
                <button onClick={handleResume} style={{ marginRight: '10px' }}>Resume</button>
            </div>
            
            <h2 style={{padding: '10px 20px',color: 'green',}}>Counter:{count}</h2>
            <button onClick={handleIncrement}
                style={{textAlign: "center",color: 'red',}}
            >Click Here</button>
        </div>
    );
}
