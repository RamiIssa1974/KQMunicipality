import "./globals.css";
import Header from "./components/Header";
import { StudentRegistrationProvider } from '../context/StudentRegistrationContext';

export const metadata = {
    title: 'نافذة تسجيل الطلاب',
    description: 'بوابة تسجيل الطلاب',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="ar" dir="rtl">
            <StudentRegistrationProvider>
                <body>
                    <div className="page-container">
                        <Header />  {/* This should appear ONLY here */}
                        <main>{children}</main>
                    </div>
                </body>
            </StudentRegistrationProvider>            
        </html>
    );
}
