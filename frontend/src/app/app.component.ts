import { Component } from '@angular/core';
import { LoadingComponent } from './pages/loading/loading.component';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrls: ['./app.component.scss'],
  imports:[LoadingComponent , RouterModule]
})
export class AppComponent {
  title = ' ReferralSwitch';
}
