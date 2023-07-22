import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }
  
  success(message: string | undefined, title: string | undefined){
      this.toastr.success(message, title)
  }
  
  error(message: string | undefined, title: string | undefined){
      this.toastr.error(message, title)
  }
  
  info(message: string | undefined, title: string | undefined){
      this.toastr.info(message, title)
  }
  
  warning(message: string | undefined, title: string | undefined){
      this.toastr.warning(message, title)
  }
}
