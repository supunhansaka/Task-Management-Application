export interface TaskItem {
    id: number;
    title: string;
    description?: string;
    isCompleted: boolean;
    createdAt: string;
    updatedAt?: string;
  }