import { Message } from "./message";

export interface Chat {
    guidId: string;
    name: string;
    type: number;
    messages: Message[];
    cantMessageToShow: number;
}