export const TASK_STATUSES = ['Open', 'InProgress', 'Completed', 'Blocked'] as const;
export type TaskStatus = typeof TASK_STATUSES[number];

export const TASK_PRIORITIES = ['Low', 'Medium', 'High', 'Critical'] as const;
export type TaskPriority = typeof TASK_PRIORITIES[number];
