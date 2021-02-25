import {autoinject} from 'aurelia-framework';
import { I18N } from 'aurelia-i18n';

@autoinject()
export class NavBar {

    constructor(private lang: I18N) {

    }

    setLocale(locale: string){
        this.lang.setLocale(locale).then();
    }
}