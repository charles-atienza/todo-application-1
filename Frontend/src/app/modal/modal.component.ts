import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';


@Component({
  selector: 'app-modal-content',
  templateUrl: './modal.component.html'
})
export class ModalComponent implements OnInit {
  public title: string = 'Invalid Input!';
  message: string = '';

  /**
   *
   */
  constructor(private dynamicDialogRef: DynamicDialogRef, public config: DynamicDialogConfig) {
  }

  ngOnInit() {
    this.message = this.config.data.message;
  }

  public close(): void {
    this.dynamicDialogRef.close();
  }
}
