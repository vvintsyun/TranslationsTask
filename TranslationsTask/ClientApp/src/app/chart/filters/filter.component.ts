import { KeyValue } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
})
export class FilterComponent {
  @Output()
  selectedValues = new EventEmitter<any>(true);
  @Input()
  values: KeyValue<number, string>[] = [];
  @Input()
  title: string = '';
  @Input()
  isMultiselect: boolean = false;

  _selected: number[] | null = null;
  get selected() {
    return this._selected ? this._selected : this.values.map(x => x.key);
  }
  set selected(value) {
    this._selected = value;
  }

  onSelectedChanged(selectedItems: any) {
    this.selectedValues.emit(selectedItems.value);
  }
}
