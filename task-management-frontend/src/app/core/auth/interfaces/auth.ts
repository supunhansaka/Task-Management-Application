export interface LoginInfo {
    username: string;
    password: string
}
export interface UserContext {
    isAuthenticated: boolean;
    username: string | null;
    userId: number | null;
}
