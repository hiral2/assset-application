import { DialogService } from 'aurelia-dialog';
import {autoinject} from 'aurelia-framework';
import {ErrorInformation} from './dialogs/error-information/error-information'
import { Prompt } from './dialogs/prompt/prompt';
import { Notification } from './dialogs/notification/notification';


@autoinject
export class NotificationService {

    constructor(
        private dialogService: DialogService
    ) {}

    displayError(error) {
        return this.dialogService.open({
            viewModel: ErrorInformation,
            model: error
        }).whenClosed();
    }

    prompt(message) {
        return this.dialogService.open({
            viewModel: Prompt,
            model: message
        }).whenClosed();
    }

    notify(message) {
        return this.dialogService.open({
            viewModel: Notification,
            model: message
        }).whenClosed();
    }
}