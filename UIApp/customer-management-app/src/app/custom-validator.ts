import { AbstractControl, ValidatorFn } from '@angular/forms';

export function dropdownValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    // Check if the selected value is empty or null
    if (!control.value) {
      return { required: true };
    }
    return null; // Return null if the value is valid
  };
}