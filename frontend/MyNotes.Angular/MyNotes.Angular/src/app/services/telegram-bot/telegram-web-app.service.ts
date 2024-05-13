import { EventEmitter, Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
  })
export class TelegramWebAppService {
    private readonly webApp: any;
    

    constructor(){
        this.webApp = (window as any).Telegram?.WebApp;
    }

    get isActive() {
        return this.getTelegramUserId != undefined;
    }

    get getTelegramUserId(): number | undefined {
        return this.webApp?.initDataUnsafe?.user?.id as number;
    }

    expand() {
        this.webApp.expand();
    }

    showConfirmation(text: string, action: (result: boolean) => void) {
        this.webApp.showConfirm(text, action)
    }

   
}

