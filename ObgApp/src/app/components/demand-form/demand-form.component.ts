import { Component, OnInit } from '@angular/core';
import { Globals } from 'src/app/utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-demand-form',
    templateUrl: './demand-form.component.html',
    styleUrls: ['./demand-form.component.css']
})
export class DemandFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
        Globals.selectTab = 2;
    }

}
