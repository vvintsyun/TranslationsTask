import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { CustomError } from "./custom-error";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler) {
    const customError = new CustomError();
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error && error.status == 400) {
          customError.friendlyMessage = error.error.errorMessage;
        } else {
          customError.friendlyMessage = 'An unexpected error with the server happened'
        }

        return throwError(customError);
      }))
  }
}
