import {autoinject} from "aurelia-framework";
import {DialogController} from 'aurelia-dialog';

@autoinject
export class Prompt {
    message: string;

    constructor(private controller: DialogController){
      controller.settings.lock = false;
      controller.settings.centerHorizontalOnly = true;
    }

    activate(data) {
        this.message = data;
    }
  }
  

  