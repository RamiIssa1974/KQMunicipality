"use client";

import { useEffect, useState } from "react";
import './test.css';
import { WordOmitter } from "./wordEmitter";

const title = "Patient Medical Records";
export default function TestPage() {
    return (
        <>
            <h1 header="Word Omitter"></h1>
            <div className="App">
                <WordOmitter />
            </div>
        </>
    );
}


