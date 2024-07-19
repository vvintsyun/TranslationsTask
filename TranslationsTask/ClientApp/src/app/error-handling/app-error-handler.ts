import { ErrorHandler, Injectable } from "@angular/core";
import { NotificationService } from "../services/notification.service";
import { CustomError } from "./custom-error";

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(private notificationService: NotificationService) { }

  public handleError(error: any) {
    if (error instanceof CustomError && error.friendlyMessage) {
      this.notificationService.showMessage(error.friendlyMessage);
    } else {
      console.error(error);
      this.notificationService.showMessage('An unexpected error happened');
    }
  }
}
