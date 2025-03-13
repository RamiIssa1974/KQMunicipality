export interface Student {
    id: number;
    year: number;
    name: string;

    idn: string;
    birthDate: string;

    telephone: string;
    gender: string;
    idFather: number;
    fathername: string;
    idMother: number;
    motherName: string;

    schoolName: string;
    schoolId: number;
    teacherName: string;
    teacherId: number;

    firstAlternativeSchool: string;
    firstAlternativeTeacher: string;
    secondAlternativeSchool: string;
    secondAlternativeTeacher: string;

    modified: string;
    created: string;
    agree: boolean;
    confirm: boolean;

    registrationTypeId: number;
    reason: string;

    firstAlternativeSchoolId: number;
    secondAlternativeSchoolId: number;
    firstAlternativeTeacherId: number;
    secondAlternativeTeacherId: number;

    reshoum_hetsonee_bdekaa: string;
}
