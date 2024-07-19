export interface Task {
  id: number;
  title: string;
  description: string;
  deadline: string;
  projectId: number;
  assigneeId?: number;
}
