import { DialogService } from 'aurelia-dialog';
import {autoinject} from 'aurelia-framework';
import {ErrorInformation} from 'shared/dialogs/error-information/error-information'
import { Prompt } from 'shared/dialogs/prompt/prompt';
import { Notification } from 'shared/dialogs/notification/notification';


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