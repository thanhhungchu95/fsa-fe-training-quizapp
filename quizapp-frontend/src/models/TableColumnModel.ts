import { IconProp } from "@fortawesome/fontawesome-svg-core";

export default class TableColumnModel {
    field: string = '';
    label: string = '';
    iconSort: IconProp[] = [];
    type: string = '';
    enum: any = null;
    sortabled: boolean = false;
}