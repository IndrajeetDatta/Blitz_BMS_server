import { formatDate } from '@angular/common';
import { Component, EventEmitter, HostListener, Injectable, Input, OnInit, Output } from '@angular/core';
import { NgbDateParserFormatter, NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent implements OnInit {

  @Input() date?: Date;
  @Input() setMaxDate?: Date | boolean;
  @Input() withTimePicker: boolean = false;
  @Output() datepickerEmitOnChange: EventEmitter<string> = new EventEmitter<string>();
  maxDateModel: NgbDateStruct = { year: 2100, month: 10, day: 10 };
  datePickerModel: NgbDateStruct = { year: 0, month: 0, day: 0 };
  timePickerModel: NgbTimeStruct = {hour: 0, minute: 0, second: 0};
  
  openTimePicker: Boolean = false;

  @HostListener("document:click")
  clickedOut() {
    if (this.openTimePicker)
      this.openTimePicker = false
  }

  constructor() { }

  ngOnInit(): void {
    if (this.setMaxDate) {
      if (typeof this.setMaxDate === "boolean")
        this.setMaxDate = new Date();

      this.maxDateModel = { year: this.setMaxDate.getFullYear(), month: this.setMaxDate.getMonth() + 1, day: this.setMaxDate.getDate() };
    }
    
    if (!this.date) this.date = new Date();

    this.datePickerModel.year = this.date.getFullYear();
    this.datePickerModel.month = this.date.getMonth() + 1;
    this.datePickerModel.day = this.date.getDate();
    
    this.timePickerModel.hour = this.date.getHours();
    this.timePickerModel.minute = this.date.getMinutes();
    this.timePickerModel.second = this.date.getSeconds();
  }

  toggleTimePicker(event: Event) {
    event.stopPropagation();
    this.openTimePicker = !this.openTimePicker;
  }

  displayDate() : string {
    const {day, month, year} = this.datePickerModel;
    const {minute, second, hour} = this.timePickerModel;
    
    const date = new Date(`${year}-${month}-${day} ${hour}:${minute}:${second}`);
    let formDate;
    
    if (this.withTimePicker)
      formDate = formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
    else
      formDate = formatDate(date, 'YYYY-MM-dd', 'en-US');
      
    this.datepickerEmitOnChange.emit(formDate);
    return formDate;
  }
}
