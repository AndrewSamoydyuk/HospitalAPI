import { Subject } from 'rxjs/Subject';

export interface CommonChild {
    executeAction() : any;
}
let subscription:any;
export function eventSubscriber(action: Subject<any>, handler: () => void, off: boolean = false) {
    if (off && subscription) {
        subscription.unsubscribe();
    } else {
        subscription = action.subscribe(() => handler());
    }
}
