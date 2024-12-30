import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
    {
        id: 1,
        label: 'MENUITEMS.MENU.TEXT',
        isTitle: true
      },
      {
        id: 2,
        label: 'Project Managment',
        icon: 'ri-dashboard-2-line',
        isCollapsed: true,
        subItems: [
          {
            id: 3,
            label: 'Dashboard',
            link: '/',
            parentId: 2
          },
          {
            id: 4,
            label: 'Hospital Registration',
            link: '/hospitals',
            parentId: 2,
            role: ['Adminstrator']
          },
          {
            id: 5,
            label: 'Clinic Registration',
            link: '/clinics',
            parentId: 2,
           role: ['Adminstrator']
          },
          {
            id: 6,
            label: 'hospital Stuff Registration',
            link: '/hospitalsStuff',
            parentId: 2,
           role: ['HospitalAdmin']
          },
          {
            id: 7,
            label: 'Register Doctors',
            link: '/hospitaldoctor',
            parentId: 2,
            role:['HospitalAdmin']
          },
          {
            id: 8,
            label: 'Clinic Stuff registration',
            link: '/clinicStuff',
            parentId: 2,
           role: ['ClinicAdmin']
          },
          {
            id: 9,
            label: 'Register Doctors',
            link: '/clinicdoctor',
            parentId: 2,
            role:['ClinicAdmin']
          },
          {
            id: 10,
            label: 'chat',
            link: '/message',
            parentId: 2,
            role:['Doctor']
          },
          {
            id: 11,
            label: 'Request Referral',
            link: '/Send-referral',
            parentId: 2,
            role:['Doctor']
          },
       
        ]
      },
      {
        id: 8,
        label: 'Case Managment',
        icon: 'ri-apps-2-line',
        isCollapsed: true,
        subItems: [
    
          {
            id: 9,
            label: 'Dashboard',
            parentId: 8,
            link:'/dashboard',
          },
    
          {
            id: 10,
            label: 'My case List',
            link: '/my-case',
            parentId: 8
          },
          {
            id: 11,
            label: 'Appointment',
            parentId: 8,
            link:'/appointment'
          },
          {
            id: 12,
            label: 'Inside Cases',
            parentId: 8,
            link:'/inside'
          },
          {
            id: 23,
            label: 'Encoded Case',
            parentId: 8,
            link: '/encode'
          },
          {
            id: 27,
            label: 'Search Case',
            parentId: 8,
            link: '/search'
          },
          {
            id: 31,
            label: 'Issue Case',
            parentId: 8,
            link: '/issue'
          },
          {
            id: 36,
            label: 'Completed Case',
            parentId: 8,
            link: '/completed'
          },
          {
            id: 42,
            label: 'Archived Case',
            parentId: 8,
            link: '/archive'
          },
          {
            id: 46,
            label: 'List of Unsent Message',
            parentId: 8,
            link: 'unsend-message'
          }
        ]
      },
];
