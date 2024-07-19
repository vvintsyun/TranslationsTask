import { Injectable, NgZone} from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private snackBar: MatSnackBar, private zone: NgZone) { }

  showMessage(message: string) {
    this.zone.run(() => {
      this.snackBar.open(message, 'x', {
        duration: 2000,
        verticalPosition: 'top',
        horizontalPosition: 'right'
      });
    });    
  }
}
