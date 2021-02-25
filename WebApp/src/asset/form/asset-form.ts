import {autoinject} from 'aurelia-dependency-injection';
import { Asset } from "asset/asset-model";
import {validateTrigger, ValidationController, ValidationControllerFactory, ValidationRules} from 'aurelia-validation';
import { BootstrapFormRenderer } from 'shared/renderers/bootstrap-form-renderer';
import { AssetService } from 'asset/asset-service';
import {Router} from 'aurelia-router';
import * as moment from 'moment';
import { CountryService } from 'country/country-service';
import { NotificationService } from 'shared/notification-service'
import { Country } from 'country/country-model';

@autoinject
export class AssetForm {
    controller: ValidationController;
    asset: Asset;
    loading = false;
    isValid = false;

    countries: Country[] = [];
    filteredCountries: Country[] = [];

    constructor(
        controllerFactory: ValidationControllerFactory,
        private route: Router,
        private assetService: AssetService,
        private countryService: CountryService,
        private notificationService: NotificationService
    ) {
        this.controller = controllerFactory.createForCurrentScope();
        this.controller.validateTrigger = validateTrigger.changeOrBlur;
        this.controller.addRenderer(new BootstrapFormRenderer());

        this.setRules();

        this.controller.subscribe((validation) => {
            this.isValid = validation.errors.length === 0;
        })
    }  

    async activate(params) {
        this.loadContries();
        if (params.id) {
            this.loading = true;
            this.assetService.find(params.id).then(asset=>{
                this.setAsset(asset);
                this.loading = false;
            }).catch(async (e) => {
                await this.notificationService.displayError(e);
                this.route.navigate('assets/list');
                this.loading = false;
            });
        
        } else {
            this.setAsset(new Asset());
        }
    }

    async loadContries() {
        this.countries = await this.countryService.getList('');
    }

    refreshFilteredCountries() {
        if(!this.asset.CountryOfDepartment || this.countries.some(c=> c.Name.toLowerCase() == this.asset.CountryOfDepartment.toLowerCase())) {
            this.filteredCountries = [];
        } else {
            this.filteredCountries = this.countries.filter(c=> c.Name.toLowerCase().indexOf(this.asset.CountryOfDepartment.toLowerCase()) >= 0);
            this.filteredCountries = this.filteredCountries.slice(0, 5);
        }
    }

    setAsset(asset: Asset) {
        if (this.asset) {
            this.controller.removeObject(this.asset);
        }

        this.asset = asset;
        this.controller.addObject(this.asset);
    }

    setCountry(countryName){
        this.asset.CountryOfDepartment = countryName;
        this.filteredCountries = [];
    }

    async reset() {
        const result = await this.notificationService
            .prompt('asset.form.reset_prompt_msg');

        if (!result.wasCancelled){
            this.setAsset(new Asset());
        }
    }
    
    setRules() {
        ValidationRules
        .ensure((asset :Asset ) => asset.AssetName)
            .required()
            .minLength(5)
            .withMessageKey('minLength_5')
            .maxLength(1000)
            .withMessageKey('maxLength_1000')
        .ensure((asset :Asset ) => asset.Department)
            .required()
            .maxLength(1000)
            .withMessageKey('maxLength_1000')
        .ensure((asset :Asset ) => asset.CountryOfDepartment)
            .required()
            .maxLength(1000)
            .withMessageKey('maxLength_1000')
            .satisfies(async (value: string) => {
                if (this.countries.length>0) {
                    return this.countries.some(c=>c.Name.toLowerCase() == value.toLowerCase())
                }
                try {
                    const country = await this.countryService.find(value);
                    return country !== undefined && country !== null;
                } catch {
                    return false;
                }
            }).withMessageKey('invalid_department_country')
        .ensure((asset :Asset ) => asset.EMailAddressOfDepartment)
            .required()
            .email()
        .ensure((asset :Asset ) => asset.PurchaseDate)
            .required()
            .satisfies((value: any) => {
               return moment().diff(moment(value), 'years')<1;
            }).withMessageKey('invalid_purchase_date_year')
        .on(Asset);
    }

    isCreateNew() {
        return !this.asset.Id;
    }

    async submit() { 
       const result = await this.controller.validate();
       if (!result.valid) {
           return;
       }

       this.loading = true;
       try {
            const data = {
                ...this.asset,
                PurchaseDate: moment(this.asset.PurchaseDate).utc().toDate()
            };
            if (this.isCreateNew()) {
               await this.assetService.create(data);
               await this.notificationService.notify('asset.form.new_created');
            } else {
                await this.assetService.update(data);
                await this.notificationService.notify('asset.form.updated');
            }

           this.route.navigate('assets/list');           
       } catch (e) {
           this.notificationService.displayError(e);
       } finally {
        this.loading = false;
       }

    }

    get isResetEnabled() {
        return Object.entries(this.asset).some(c=>c[1]);
    }
}


  