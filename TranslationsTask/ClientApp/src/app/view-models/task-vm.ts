export interface TaskVm {
  id: number;
  title: string;
  description: string;
  deadline: string;
  project: string;
  assignee?: string;
}
