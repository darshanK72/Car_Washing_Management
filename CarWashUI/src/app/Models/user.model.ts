export interface User {
    userId: number;
    adminId?:number;
    washerId?:number;
    name: string;
    email: string;
    isActive: boolean;
    password: string;
    phoneNumber: string;
    role: string;
    profilePicture?: string;
    address?: string;
  }
  