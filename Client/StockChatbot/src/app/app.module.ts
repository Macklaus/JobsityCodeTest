import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ScrollToBottomDirective } from './directive/scroll-to-bottom.directive';

@NgModule({
  declarations: [
    AppComponent,
    ScrollToBottomDirective
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatCardModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatIconModule,
    FormsModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
