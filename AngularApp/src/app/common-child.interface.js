"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var subscription;
function eventSubscriber(action, handler, off) {
    if (off === void 0) { off = false; }
    if (off && subscription) {
        subscription.unsubscribe();
    }
    else {
        subscription = action.subscribe(function () { return handler(); });
    }
}
exports.eventSubscriber = eventSubscriber;
//# sourceMappingURL=common-child.interface.js.map