export default interface LinkMessage {
    infos: DisplayEmoteInfo[];
}

export interface DisplayEmoteInfo {
    uri: string;
    wbh: number;
}