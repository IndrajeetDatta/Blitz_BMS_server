export interface Station {
  serial_number: number;
  connected: boolean,
  client: string;
  address: string;
  installer: string;
  status: number[];
  configuration: string;
  heartbeat: string;
  software_version: string;
  installer_phone: string;
  client_phone: string;
  client_email: string;
  network: string;
  date_installed: string;
  up_time: string;
  system_timer: string;
  sim_active: boolean;

}
