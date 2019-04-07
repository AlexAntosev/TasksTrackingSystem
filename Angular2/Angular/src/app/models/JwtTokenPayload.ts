import { UserRole } from 'src/app/models/user-role';

export interface JwtTokenPayload {
    subjectId: string;
    issuedAtMs: number;
    expirationDateMs: number;
    issuer: string;
    audience: string;
    role: UserRole;
}