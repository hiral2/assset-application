import { Asset } from "asset/asset-model";
import { AssetService } from "asset/asset-service";
import {autoinject} from 'aurelia-dependency-injection';
import { NotificationService } from "shared/notification-service";

@autoinject
export class AssetList {
    list: Asset[];
    loading = false;
    retry = false;
    deleting = false;

    constructor(
        private assetService: AssetService,
        private notificationService: NotificationService
    ) { }

    async activate() {
       this.load();
    }

    async load() {
        this.loading = true;
        try {
            this.list = await this.assetService.getList();
        } catch(e) {
            this.retry = true;
            await this.notificationService.displayError(e);
        } finally {
            this.loading = false;
        }
    }

    async delete(asset: Asset) {
        const result = await this.notificationService.prompt("asset.list.delete_asset_confirmation");

        if(result.wasCancelled) {
            return;
        }

        try {
            await this.assetService.delete(asset.Id);
            this.load();
        } catch (e) {
            await this.notificationService.displayError(e);
        }
    }
}