"use client";

import Link from "next/link";
import Image from "next/image";

const Header = () => {
    return (
        <header className="header">
            <div className="header-content">
                {/* Left Logo */}
                <Image
                    src="/Images/left-logo.png"
                    alt="Left Logo"
                    width={100}
                    height={100}
                    priority
                />

                {/* Title */}
                <Link href="/HomePage" className="header-title">
                    نافذة تسجيل الطلاب
                </Link>               
                {/* Right Logo */}
                <Image
                    src="/Images/right-logo.png"
                    alt="Right Logo"
                    width={100}
                    height={100}
                    priority
                />
            </div>
        </header>
    );
};

export default Header;
