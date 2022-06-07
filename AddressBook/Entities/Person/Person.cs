﻿namespace AddressBook.Entities.Person;

public record Person(Guid Id, string FirstName, string LastName,
    Address[] Addresses, PhoneNumber[] PhoneNumbers, SocialMedia[] SocialMedia,
    bool Archived);

