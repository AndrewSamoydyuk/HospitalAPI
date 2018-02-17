import { Directive, HostListener, Input } from '@angular/core';
declare var $: any;

@Directive({
    selector: '[appearByClick]'
})
export class ClickDirective {

    @Input() elementId: string;

    @HostListener('click') onMouseClick() {
        if ($('#' + this.elementId).css('display')==='none') {
            $('#'+this.elementId).fadeIn(600);
        } else {
            $('#' +this.elementId).fadeOut(600);
        }
    }

} 