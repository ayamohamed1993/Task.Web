import { Component } from '@angular/core';
import { TransferService } from '../services/transfer.service';


@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.css']
})
export class TransferComponent {
  /**
   *
   */
  model:TransferDto;
  constructor(private transferService:TransferService) {
      
  }
  
  // users: User[] ;
  // senderMobileNumber: string;
  // receiverMobileNumber: string;
  // transferAmount: number;

  transferBalance(): void {

  
  this.transferService.tranferBalance("/api/transfer/index",this.model)

  }
}