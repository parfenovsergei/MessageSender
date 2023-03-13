import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup , Validators} from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-code-dialog',
  templateUrl: './confirm-code-dialog.component.html',
  styleUrls: ['./confirm-code-dialog.component.css']
})
export class ConfirmCodeDialogComponent {
  codeForm!: FormGroup

  constructor(@Inject(MAT_DIALOG_DATA) public data: number,
  private matDialogRef: MatDialogRef<ConfirmCodeDialogComponent>){
    this.codeForm = new FormGroup({
      Code: new FormControl(0, [
        Validators.required
      ])
  })}

  onCloseClick(){
    this.matDialogRef.close();
  }
}
