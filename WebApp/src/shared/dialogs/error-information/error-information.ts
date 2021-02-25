import {autoinject} from "aurelia-framework";
import {DialogController} from 'aurelia-dialog';

@autoinject
export class ErrorInformation {
    error: any;
    message: string;

    constructor(private controller: DialogController){
      controller.settings.lock = false;
      controller.settings.centerHorizontalOnly = true;
    }

    activate(error) {
        this.error = error;
        if(this.error && (this.error.message || this.error.title)) {
          this.message = "error."+String(this.error.message || this.error.title).toLowerCase().replace(/ /gi,'_');
        } else if (this.error && this.error.status) {
          this.message = "error.http_code_"+this.error.status;
        }
    }
  }
  