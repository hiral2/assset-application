import 'intl';
import 'whatwg-fetch';

import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {ValidationMessageProvider} from 'aurelia-validation';
import {PLATFORM} from 'aurelia-pal';
import {I18N, TCustomAttribute} from 'aurelia-i18n';
import Backend from 'i18next-xhr-backend'; // <-- your previously installed backend plugin
  
export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use
    .plugin(PLATFORM.moduleName('aurelia-testing'))
  }
  aurelia.use
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'), config => {
      config.settings.startingZIndex = 5;
      config.settings.keyboard = true;     
      config.useDefaults();
    })
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
      let aliases = ['t', 'i18n'];
      // add aliases for 't' attribute
      TCustomAttribute.configureAliases(aliases);

      // register backend plugin
      instance.i18next.use(Backend);
      // instance.i18next.use(Backend.with(aurelia.loader));

      // adapt options to your needs (see http://i18next.com/docs/options/)
      // make sure to return the promise of the setup method, in order to guarantee proper loading
      return instance.setup({
        backend: {                                  // <-- configure backend settings
          loadPath: './locales/{{lng}}/{{ns}}.json', // <-- XHR settings for where to get the files from
        },
        preload: ['en', 'de'],
        attributes: aliases,
        lng : 'en',
        fallbackLng : 'de',
        debug : false,
      })
    });

  ValidationMessageProvider.prototype.getMessage = function(key) {
    const i18n = aurelia.container.get(I18N);
    const translation = i18n.tr(`errorMessages.${key}`);
    return this.parser.parse(translation);
  };

  ValidationMessageProvider.prototype.getDisplayName = (propertyName, displayName) => {
    if (displayName !== null && displayName !== undefined) {
      return displayName as string;
    }
    const i18n = aurelia.container.get(I18N);
    return i18n.tr(propertyName as string);
  };

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
