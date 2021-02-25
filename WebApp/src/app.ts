import 'bootstrap/dist/css/bootstrap.min.css';
import  'bootstrap';

import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from "aurelia-framework";

export class App {
  router: Router;
  
    configureRouter(config: RouterConfiguration, router: Router): void {
      config.map([
        { route: '', name: 'assets', moduleId:  PLATFORM.moduleName('asset/list/asset-list'), title: 'Home' },
        { route: 'assets/list', name: 'assetList', moduleId:  PLATFORM.moduleName('asset/list/asset-list'), title: 'Asset List'},
        { route: 'assets/form/:id?', name: 'assetFormCreate', moduleId:  PLATFORM.moduleName('asset/form/asset-form'), title: 'Asset Form' },
      ]);
      this.router = router;
    }
}
