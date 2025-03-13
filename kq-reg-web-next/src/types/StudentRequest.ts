export interface StudentRequest {
    reshoum_hetsonee_bdekaa: string;
    endPoint: string;

    year: number;
    idn: string;
    birthDate: string;

    agree: string; // ✅ STRING ("Yes"/"No")

    schoolName: string;
    schoolId: number;
    teacherName: string;
    teacherId: number;

    firstAlternativeSchool: string;
    firstAlternativeTeacher: string;
    secondAlternativeSchool: string;
    secondAlternativeTeacher: string;

    reason: string;

    registration_Type: number;
}
