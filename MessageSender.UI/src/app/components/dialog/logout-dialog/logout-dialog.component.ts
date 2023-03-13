import { Component, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-logout-dialog',
  templateUrl: './logout-dialog.component.html',
  styleUrls: ['./logout-dialog.component.css']
})
export class LogoutDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: number,
  private matDialogRef: MatDialogRef<LogoutDialogComponent>){}

  onCloseClick(){
    this.matDialogRef.close();
  }
}
