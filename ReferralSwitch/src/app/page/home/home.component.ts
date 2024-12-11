import { Component, OnInit, HostListener, ElementRef, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HeaderComponent } from '../../layout/header/header.component';
import { FooterComponent } from '../../layout/footer/footer.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent,FooterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  private hasAnimated = false;

  constructor(
    private elRef: ElementRef,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.checkIfInView();
    }
  }

  @HostListener('window:scroll', [])
  onWindowScroll(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.checkIfInView();
    }
  }

  private checkIfInView(): void {
    const statsSection = this.elRef.nativeElement.querySelector('.statistics-section');
    if (statsSection) {
      const rect = statsSection.getBoundingClientRect();
      const windowHeight = window.innerHeight || document.documentElement.clientHeight;

      if (rect.top <= windowHeight && rect.bottom >= 0 && !this.hasAnimated) {
        this.animateCounters();
        this.hasAnimated = true;
      }
    }
  }

  private animateCounters(): void {
    const counters = this.elRef.nativeElement.querySelectorAll('.count');
    const speed = 200;

    counters.forEach((counter: HTMLElement) => {
      const target = parseInt(counter.getAttribute('data-count') || '0', 10);
      const updateCount = () => {
        const current = parseInt(counter.textContent || '0', 10);
        const increment = Math.ceil(target / speed);

        if (current < target) {
          counter.textContent = `${current + increment}`;
          setTimeout(updateCount, 20);
        } else {
          counter.textContent = `${target}`;
        }
      };
      updateCount();
    });
  }
}
