import { Component, inject} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Project } from '../../models/project';
import { MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.sass']
})
export class AddProjectComponent {
  
  readonly dialogRef = inject(MatDialogRef<AddProjectComponent>);
  projectForm = new FormGroup({
    name: new FormControl('')
  });

  getErrorMessage(formControl: any) {
    if (formControl.hasError('required')) {
      return 'You must enter a value';
    }

    return '';
  }

  onSubmit() {
    const data = this.projectForm.value as Project;
    this.dialogRef.close(data);
  }
}
