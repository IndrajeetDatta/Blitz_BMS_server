import { Component, OnInit, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-cell-table-station-configuration',
  templateUrl: './cell-table-station-configuration.component.html',
  styleUrls: ['./cell-table-station-configuration.component.scss']
})
export class CellTableStationConfigurationComponent implements OnInit {

  constructor() { }

  @Input() label: string;
  @Input() value?: string  | number;
  @Input() edit : boolean;
  @Input() textContol: FormControl;
  @Input() passwordTypeActive: boolean
  
  ngOnInit(): void {
    if (!this.value) this.value = "";
    else if (typeof this.value === "number") this.value = this.value.toString();
  }

}
