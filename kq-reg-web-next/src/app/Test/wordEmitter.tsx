import React, { useState } from "react";

const OMITTED_WORDS = ["a", "the", "and", "or", "but"];

function WordOmitter() {
    const [inputText, setInputText] = useState("");
    const [omittedText, setOmittedText] = useState("");
    const [omitWords, setOmitWords] = useState(true);

    const handleInputChange = (e) => {
        setInputText(e.target.value);
    };

    const toggleOmitWords = () => {
        setOmitWords(!omitWords);
    };

    const clearFields = () => {
        setInputText('');
    };

    const getProcessedText = () => {
        const words = inputText.split(" ");

        var res = words.filter(w => !OMITTED_WORDS.includes(w));

        return res.map(w => w + " ");
    };

    return (        
        <div className="omitter-wrapper">
            <div>{OMITTED_WORDS.map(w=>w+" ")}</div>
            <textarea
                placeholder="Type here..."
                value={inputText}
                onChange={handleInputChange}
                data-testid="input-area"
            />
            <div>
                <button onClick={toggleOmitWords} data-testid="action-btn">
                    {omitWords ? "Show All Words" : "Omit Words"}
                </button>
                <button onClick={clearFields} data-testid="clear-btn">
                    Clear
                </button>
            </div>
            <div>
                <h2>Output:</h2>
                <p data-testid="output-text">
                    {!omitWords ? inputText : getProcessedText()}</p>
            </div>
        </div>
    );
}

export { WordOmitter };
